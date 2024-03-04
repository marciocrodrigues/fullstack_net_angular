import { AbstractControl, FormControl, FormGroup } from "@angular/forms";

export class FormularioHelper {
    static mensagemValidacaoFormulario(
        form: FormControl,
        campo: string,
        min: number = 0,
        max: number = 0,
        descricao: string = ''): string {
        var campoForm = form;
        var campoNome = `${campo[0].toUpperCase()}${campo.slice(1, campo.length)}`
        var nomeCampo = `${descricao ? descricao : campoNome}`;
    
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
        
        if (campoForm?.hasError('mustMatch')) {
          return `As senhas devem ser iguais`; 
        }

        return '';
    }

    static validarFormulario(form: FormControl): boolean | undefined | null {
      return form.errors && form.touched;
    }

    static MustMatch(controlName: string, matchingControlName: string): any {
      return (group: AbstractControl) => {
        const formGroup = group as FormGroup;
        const control = formGroup.controls[controlName];
        const matchingControl = formGroup.controls[matchingControlName];

        if (matchingControl.errors && !matchingControl.errors.mustMatch) {
          return null;
        }

        if (control.value !== matchingControl.value) {
          matchingControl.setErrors({ mustMatch: true });
        } else {
          matchingControl.setErrors(null);
        }

        return null;
      }
    }
}