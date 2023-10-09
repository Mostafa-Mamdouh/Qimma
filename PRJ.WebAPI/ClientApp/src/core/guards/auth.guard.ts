import { Injectable } from '@angular/core';

import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router,
} from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AccountService } from 'src/app/services/Account/account.service';
import { SecurityService } from '../security/security.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private accountService: AccountService,
    private jwtHelper: JwtHelperService,
    private router: Router,
    private securityService: SecurityService
  ) {}

  async canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Promise<boolean> {
    const token = this.accountService.getJwtToken();
    try {
      if (token && !this.jwtHelper.isTokenExpired(token)) {
        return true;
      } else {
        this.router.navigate(['login'], {
          queryParams: { returnUrl: state.url },
        });
        return false;
      }
    } catch (ex) {
      this.router.navigate(['login'], {
        queryParams: { returnUrl: state.url },
      });
      return false;
    }
  }
}
