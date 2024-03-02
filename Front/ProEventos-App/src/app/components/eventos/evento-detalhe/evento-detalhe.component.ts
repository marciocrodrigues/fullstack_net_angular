import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {

  public formulario!: FormGroup;

  get f(): any {
    return this.formulario.controls;
  }

  constructor(
    private readonly fb: FormBuilder
  ) { }

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
    return this.f[campo]?.errors && this.f[campo]?.touched;
  }

  public mensagemValidacaoFormulario(campo: string, min: number = 0, max: number = 0, descricao: string = ''): string {
    var campoForm = this.f[campo];
    var nomeCampo = `${descricao ? descricao : campo[0].toUpperCase()}${campo.slice(1, campo.length)} `;

    if (campoForm?.hasError('required')) {
      return `${nomeCampo} é obrigatório`;
    }

    if (campoForm?.hasError('maxlength') && max > 0) {
      return `${nomeCampo} deve ter no maximo ${max} caracteres`;
    }

    if (campoForm?.hasError('minlength') && min > 0) {
      return `${nomeCampo} deve ter no minimo ${min} caracteres`;
    }

    if (campoForm?.hasError('max') && max > 0) {
      return `${nomeCampo} deve ser menor ou igual a ${max}`;
    }

    if (campoForm?.hasError('email')) {
      return `${nomeCampo} deve ser um e-mail valido`;
    }

    return '';
  }

}
