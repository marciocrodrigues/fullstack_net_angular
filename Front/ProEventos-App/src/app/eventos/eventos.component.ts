import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit, TemplateRef } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EventoService } from '../services/evento.service';
import { Evento } from '../models/Evento';
import { Subscription } from 'rxjs';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit, OnDestroy {
  modalRef?: BsModalRef;

  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];

  public exibirImagem = false;
  private filtroListado = '';

  private subscriptions = new Subscription();

  constructor(
    private readonly modalService: BsModalService,
    private readonly eventoService: EventoService
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
    this.subscriptions.add(
      this.eventoService.BuscarEventos().subscribe({
          next: (eventosResp: Evento[]) => {
            this.eventos = eventosResp;
            this.eventosFiltrados = eventosResp;
          },
          error: (error: any) => console.log(error)
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

}
