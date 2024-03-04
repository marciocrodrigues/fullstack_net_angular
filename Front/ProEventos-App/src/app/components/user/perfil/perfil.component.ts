import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FormularioHelper } from '@app/helpers/FormularioHelper';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {
  public formulario!: FormGroup;

  get f(): any {
    return this.formulario?.controls;
  }

  constructor(
    private readonly fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.iniciarFormulario();
  }

  private iniciarFormulario() {
    const formOptions: AbstractControlOptions = {
      validators: FormularioHelper.MustMatch('senha', 'confirmeSenha')
    };

    this.formulario = this.fb.group({
      titulo: ['', [
        Validators.required
      ]],
      primeiroNome: ['', [
        Validators.required
      ]],
      ultimoNome: ['', [
        Validators.required
      ]],
      email: ['', [
        Validators.required,
        Validators.email
      ]],
      telefone: ['', [
        Validators.required
      ]],
      funcao: ['', [
        Validators.required
      ]],
      descricao: ['', [
        Validators.required
      ]],
      senha: ['', [
        Validators.required,
        Validators.minLength(6)
      ]],
      confirmeSenha: ['', [
        Validators.required
      ]]
    }, formOptions);
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
