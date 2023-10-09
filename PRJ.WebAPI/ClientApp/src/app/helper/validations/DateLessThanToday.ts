import {
  AbstractControl,
  FormControl,
  ValidationErrors,
  ValidatorFn,
} from '@angular/forms';

export function DateLessThanToday(
  control: AbstractControl
): { [key: string]: boolean } | null {
  let today: Date = new Date();
  if (new Date(control.value) > today || new Date(control.value) == today)
    return { dateLessThanToday: false };

  return null;
}
