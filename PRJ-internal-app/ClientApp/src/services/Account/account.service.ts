import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { AppSettings } from 'src/AppSettings';
import UserData, { TokenDto } from 'src/models/Common/userdata';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl: string = AppSettings.WebApiUrl;
  private readonly JWT_TOKEN = 'JWT_TOKEN';
  securityObject: UserData = new UserData();
  private loggedIn = new BehaviorSubject<boolean>(false);
  private currentUserSource = new BehaviorSubject<UserData>(null);
  currentUser$ = this.currentUserSource.asObservable();

  get isLoggedIn() {
    return this.loggedIn.asObservable();
  }

  constructor(private http: HttpClient, private route: Router, private jwtHelper: JwtHelperService) {}
  getCurrentUserValue() {
    return this.currentUserSource.value;
  }
  getUserDetail() {
    const user = this.jwtHelper.decodeToken(this.getJwtToken());
    const userDetails = new UserData();
    userDetails.username = user.Username;
    
    return userDetails;
  }


  async loadCurrentUser() {
    const token = this.getJwtToken();
    try {
      if (token && !this.jwtHelper.isTokenExpired(token)) {
        this.loggedIn.next(true);
        var userData = this.getUserDetail();
        this.currentUserSource.next(userData);
        return true;
      } else {
    
          this.removeTokens();
          return false;
      }
    } catch (ex) {
      this.logout();
      this.route.navigateByUrl('/login');
      return false;
    }
  }



  login(values: any) {
    return this.http
      .post<any>(`${this.baseUrl}api/account/login`, values)
      .pipe(
        map((user: any) => {
          console.log(user)

          if (user.data) {
            var tokenDto = new TokenDto();
            tokenDto.accessToken = user.data.accessToken;

            this.storeTokens(tokenDto);
          }
          return user;
        })
      );
  }

  private storeTokens(token: TokenDto) {
    localStorage.setItem(this.JWT_TOKEN, token.accessToken);
    this.loggedIn.next(true);
    this.loadCurrentUser();
  }

  logout() {
    this.removeTokens();
    this.currentUserSource.next(null);
    this.route.navigateByUrl('/login');
  }

  getJwtToken() {
    return localStorage.getItem(this.JWT_TOKEN);
  }

  private removeTokens() {
    localStorage.removeItem(this.JWT_TOKEN);
    localStorage.clear();
    this.loggedIn.next(false);
    this.route.navigateByUrl('/login');
  }


}
