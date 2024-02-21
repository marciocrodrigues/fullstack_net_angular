import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  public eventos: any;

  constructor(
    private readonly http: HttpClient
  ) { }

  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos(): void {
    this.http.get(`${environment.baseUrl}eventos`).subscribe(
      response => this.eventos = response,
      error => console.log(error)
    );
  }

}
