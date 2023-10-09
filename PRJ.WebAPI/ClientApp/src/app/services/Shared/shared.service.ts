import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SharedService {
  public lang = new BehaviorSubject<string>('en');

  markFormGroupTouched(formGroup: FormGroup) {
    (<any>Object).values(formGroup.controls).forEach((control: FormGroup) => {
      control.markAsTouched();

      if (control.controls) {
        this.markFormGroupTouched(control);
      }
    });
  }

  public currentEntity = new BehaviorSubject<any>(null);
  public currentFacility = new BehaviorSubject<any>(null);
  public currentLicense = new BehaviorSubject<any>(null);
}
