import { Injectable } from '@angular/core';
import { FormArray } from '@angular/forms';
import { Helper, ParameterClass } from '../../helper/helper';
import { LookupSet } from '../../models/LookupSet/LookupSet';
//import * as CryptoJS from 'crypto-js';
@Injectable({
  providedIn: 'root',
})
export class CommonClass {
  encryptSecretKey: string ="HIGHsecuredEncryptionForIds";

  constructor() { }

  OnlyArabic(event: any, lng: any) {
    if (event.target.value.length >= lng) {
      return false;
    }
    var arabicAlphabetAndWhiteSpace = /[\u0600-\u06FF\_\@\&\$\/\)\(\. ]/;
    var key = String.fromCharCode(event.which);
    if (event.keyCode == 8 || event.keyCode == 37 || event.keyCode == 39 || event.keyCode == 32 || arabicAlphabetAndWhiteSpace.test(key)) {
      return true;
    }
    return false;
  }


  OnlyEnglish(event: any, lng: any) {
    if (event.target.value.length >= lng) {
      return false;
    }
    var englishAlphabetAndWhiteSpace = /[A-Za-z\-\_\@\&\$\/\)\(\. ]/g;
    var key = String.fromCharCode(event.which);
    if (event.keyCode == 8 || event.keyCode == 37 || event.keyCode == 39 || englishAlphabetAndWhiteSpace.test(key)) {
      return true;
    }
    return false;
  }


  OnlyEnglishCode(event: any, lng: any) {
    if (event.target.value.length >= lng) {
      return false;
    }
    var englishAlphabetAndWhiteSpace = /[a-zA-Z0-9\@\&\$\/\)\(\. ]/g;
    var key = String.fromCharCode(event.which);
    if (event.keyCode == 8 || event.keyCode == 37 || event.keyCode == 39 || englishAlphabetAndWhiteSpace.test(key)) {
      return true;
    }
    return false;
  }



  isNumberKey(evt: any) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
      return false;
    return true;
  }

  onSubmitEditedProfile(form: FormArray) {
    const formValue: any[] = [];
    // iterate over form controls no matter how many control you have.
    Object.keys(form.controls).map((key) => {
      // create a new parsed object
      if (form.get(key).dirty) {
        const parsedValue = form.get(key).value;
        // changed: form.get(key).dirty, // added changed key to identify value change
        formValue.push(parsedValue);
      }

      // push each parsed control to formValue array.
    });

    return formValue;
  }

  //encryptData(data) {

  //  try {
  //    return CryptoJS.AES.encrypt(JSON.stringify(data), this.encryptSecretKey).toString();
  //  } catch (e) {
  //    console.log(e);
  //  }
  //}
  

  //decryptData(data) {

  //  try {
  //    const bytes = CryptoJS.AES.decrypt(data, this.encryptSecretKey);
  //    if (bytes.toString()) {
  //      return JSON.parse(bytes.toString(CryptoJS.enc.Utf8));
  //    }
  //    return data;
  //  } catch (e) {
  //    console.log(e);
  //  }
  //}

}
