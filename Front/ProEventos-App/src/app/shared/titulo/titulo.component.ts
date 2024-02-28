import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent implements OnInit {
  @Input() titulo  = '';
  @Input() rotaLista = '';
  @Input() subtitulo = '';
  @Input() iconClass = '';
  @Input() botaoListar = false;

  constructor(
    private readonly router: Router
  ) { }

  ngOnInit(): void {
  }

  listar(): void {
    if (this.rotaLista !== '') {
      this.router.navigate([`/${this.rotaLista}`])
    }
  }
}
