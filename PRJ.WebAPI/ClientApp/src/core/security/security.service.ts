import { Injectable } from '@angular/core';
import { Router, RouterStateSnapshot } from '@angular/router';
import { map } from 'rxjs/operators';
import { AccountService } from 'src/app/services/Account/account.service';

@Injectable({
  providedIn: 'root',
})
export class SecurityService {
  constructor(private accountService: AccountService, private router: Router) {}
  private isClaimValid(claim: string): boolean {
   
    return true;
    // Retrieve security object
  }

  hasClaim(claim: any): boolean {
    let ret: boolean = false;
    // See if an array of values was passed in.
    return ret;
  }
}
