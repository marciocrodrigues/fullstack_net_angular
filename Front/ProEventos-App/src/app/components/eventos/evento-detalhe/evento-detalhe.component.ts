import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { FormularioHelper } from '@app/helpers/FormularioHelper';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';
import { moment } from 'ngx-bootstrap/chronos/testing/chain';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit, OnDestroy {

  public formulario!: FormGroup;
  private id!: number;
  private subscriptions = new Subscription();
  evento = {} as Evento;

  get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false
    }
  };

  get f(): any {
    return this.formulario.controls;
  }

  constructor(
    private readonly fb: FormBuilder,
    private readonly activeRoute: ActivatedRoute,
    private localeService: BsLocaleService,
    private readonly eventoService: EventoService,
    private readonly toastServicce: ToastrService,
    private readonly spinnerService: NgxSpinnerService
  ) { 
    this.activeRoute.params.subscribe((params) => {
      this.id = params.id;
    });
    this.localeService.use('pt-br');
  }
  
  ngOnInit(): void {
    if (this.id) {
      this.carregarEvento();
    }
    this.iniciarFormulario();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  public carregarEvento(): void {
    this.spinnerService.show();
    this.subscriptions.add(
      this.eventoService.BuscarEventoPorId(this.id).subscribe(
        {
          next: (eventosResp: Evento) => {
            if (eventosResp) {
              this.evento = {...eventosResp};
              this.formulario.patchValue(this.evento);

              if (this.evento.dataEvento) {
                const date = new Date(this.evento.dataEvento);
                console.log(`${date.getDate().toString().padStart(2, '0')}/${date.getMonth().toString().padStart(2, '0')}/${date.getFullYear()}`)
                this.formulario.get('dataEvento')?.setValue(`${date.getDate().toString().padStart(2, '0')}/${date.getMonth().toString().padStart(2, '0')}/${date.getFullYear()}`)
              }
            }
          },
          error: (error: any) => {
            this.spinnerService.hide()
            this.toastServicce.error('Erro ao carregar os Eventos', 'Erro!')
          },
          complete: () => this.spinnerService.hide()
        }
      )
    );
  }

  private iniciarFormulario() {
    this.formulario = this.fb.group({
      local: ['', [Validators.required]],
      dataEvento: ['', [Validators.required]],
      tema: ['', [
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(50)
      ]],
      qtdPessoas: ['', [
        Validators.required,
        Validators.max(120000)
      ]],
      imageURL: ['', [Validators.required]],
      telefone: ['', [Validators.required]],
      email: ['', [
        Validators.required,
        Validators.email
      ]]
    });
  }

  public validarFormulario(campo: string): boolean | undefined | null {
    return FormularioHelper.validarFormulario(this.f[campo]);
  }

  public mensagemValidacaoFormulario(campo: string, min: number = 0, max: number = 0, descricao: string = ''): string {
    return FormularioHelper.mensagemValidacaoFormulario(
      this.f[campo],
      campo,
      min,
      max,
      descricao
    );
  }

}
