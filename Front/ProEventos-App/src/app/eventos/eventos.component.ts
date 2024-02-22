import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  public eventos: any = [];
  public eventosFiltrados: any = [];

  exibirImagem = false;
  private _filtroLista: string = '';

  constructor(
    private readonly http: HttpClient
  ) { }

  ngOnInit(): void {
    this.getEventos();
  }

  public get filtroLista(): string {
    return this._filtroLista;    
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
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
    this.http.get(`${environment.baseUrl}eventos`).subscribe(
      response =>{
        this.eventos = response;
        this.eventosFiltrados = response;
      },
      error => console.log(error)
    );
  }

  toogleImage() {
    this.exibirImagem = !this.exibirImagem;
  }

}
