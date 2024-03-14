import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Evento } from "../models/Evento";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'
})
export class EventoService {
    baseUrl: string = '';

    constructor(
        private readonly http: HttpClient
    ){
        this.baseUrl = `${environment.baseUrl}eventos`;
    }

    public BuscarEventos(): Observable<Evento[]> {
        return this.http.get<Evento[]>(this.baseUrl);
    }

    public BuscarEventosPorTema(tema: string): Observable<Evento[]> {
        return this.http.get<Evento[]>(`${this.baseUrl}/${tema}/tema`);
    }

    public BuscarEventoPorId(id: number): Observable<Evento> {
        return this.http.get<Evento>(`${this.baseUrl}/${id}`);
    }

    public postEvento(evento: Evento): Observable<Evento> {
        return this.http.post<Evento>(this.baseUrl, evento);
    }

    public putEvento(id: number, evento: Evento): Observable<Evento> {
        return this.http.put<Evento>(`${this.baseUrl}/${id}`, evento);
    }

    public deleteEvento(id: number): Observable<any> {
        return this.http.delete(`${this.baseUrl}/${id}`);
    }
}