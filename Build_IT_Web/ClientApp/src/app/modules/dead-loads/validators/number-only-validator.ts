import { AbstractControl, FormControl, FormGroupDirective, NgForm, ValidationErrors, ValidatorFn } from "@angular/forms";
import { ErrorStateMatcher } from "@angular/material/core";

export function numberOnlyValidator() : ValidatorFn {
  return (control: AbstractControl) : ValidationErrors | null =>{
    return {isNumber : false}
  }
}
