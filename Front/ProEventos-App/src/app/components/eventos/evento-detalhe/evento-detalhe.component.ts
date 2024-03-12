import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { FormularioHelper } from '@app/helpers/FormularioHelper';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {

  public formulario!: FormGroup;
  private id!: number;

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
    private readonly activeRoute: ActivatedRoute
  ) { 
    this.activeRoute.params.subscribe((params) => {
      this.id = params.id;
    });
    console.log(this.id)
  }

  ngOnInit(): void {
    this.iniciarFormulario();
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
      imageUrl: ['', [Validators.required]],
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
