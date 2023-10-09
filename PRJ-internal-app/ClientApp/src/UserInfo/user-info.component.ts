import { Component, forwardRef, Inject, ViewChild, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';


import { TranslateService } from '@ngx-translate/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UserInfoServices } from '../services/userinfo.service';
import { AppComponent } from '../app/app.component';


declare var $: any;

@Component({
  selector: 'user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.css']
})

export class UserInfoComponent implements OnInit, AfterViewInit {
  headerTitle: string = '';
  btnStyle = 'btn-default';

  HaveAccess: boolean = false;
  IsEdit: boolean = false;
  form: FormGroup = <FormGroup>{};
  /** addplan ctor */
  constructor(@Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private translateService: TranslateService,
    private updServices: UserInfoServices,
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private cdr: ChangeDetectorRef) {
     
    this.route.queryParams.subscribe(params => {

    });
  }
  ngOnInit(): void {

  }
  ngAfterViewInit(): void {
    
  }

}
