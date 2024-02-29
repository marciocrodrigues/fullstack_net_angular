import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '@app/services/evento.service';
import { Evento } from '@app/models/Evento';
import { Subscription } from 'rxjs';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];

  public exibirImagem = false;
  private filtroListado = '';

  private subscriptions = new Subscription();

  constructor(
    private readonly router: Router,
    private readonly modalService: BsModalService,
    private readonly eventoService: EventoService,
    private readonly toastServicce: ToastrService,
    private readonly spinnerService: NgxSpinnerService
  ) { }
  
  ngOnInit(): void {
    this.getEventos();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  public get filtroLista(): string {
    return this.filtroListado;    
  }

  public set filtroLista(value: string) {
    this.filtroListado = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  private filtrarEventos(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: { tema: string; local: string }) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  public getEventos(): void {
    this.spinnerService.show();
    this.subscriptions.add(
      this.eventoService.BuscarEventos().subscribe({
          next: (eventosResp: Evento[]) => {
            this.eventos = eventosResp;
            this.eventosFiltrados = eventosResp;
          },
          error: (error: any) => {
            this.spinnerService.hide()
            this.toastServicce.error('Erro ao carregar os Eventos', 'Erro!')
          },
          complete: () => this.spinnerService.hide()
        })
    );
  }

  public toogleImage() {
    this.exibirImagem = !this.exibirImagem;
  }

  public openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public excluir() {
    this.modalRef?.hide();
  }

  public cancelar() {
    this.modalRef?.hide();
  }

  public detalheEvento(id: number) {
    if (id !== null && id !== undefined) {
      this.router.navigate([`eventos/detalhe/${id}`]);
    }
  }

}
