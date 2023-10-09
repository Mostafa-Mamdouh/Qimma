import { Component, forwardRef, Inject, ViewChild, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faTrash, faPenToSquare, faArrowAltCircleLeft } from '@fortawesome/free-solid-svg-icons';
import { TranslateService } from '@ngx-translate/core';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { AppComponent } from '../../../app.component';
import UserData from '../../../../models/Common/userdata';
import { Response } from '../../../../models/Common/response';
import { FormControl } from '@angular/forms';
import { Subscription, window } from 'rxjs';
import { SharedService } from '../../../services/shared.service';
import { CommonClass } from '../../../common/CommonClass';
import { Title } from "@angular/platform-browser";
import { CommonServices } from '../../../../services/Common/common.service';
import { LookupSet } from '../../../../models/LookupSet/LookupSet';
import { UserInfoServices } from '../../../../services/userinfo.service';

import { RoleService } from '../../../../services/InternalRole/role.service';
import { ScreenMaster } from '../../../../models/ScreenManagement/Screen';
import { InternalRoleScreen } from '../../../../models/InternalRole/InternalRoleScreen';

declare var $: any;

@Component({
  selector: 'internal-role-screen',
  templateUrl: './internal-role-screen.component.html',
  styleUrls: ['./internal-role-screen.component.css']
})

export class InternalRoleScreenComponent implements OnInit, AfterViewInit {
  currentUser: UserData = <UserData>{};
  dataList: InternalRoleScreen[];
  screenFieldObj: any;
  ParamId: string = "";
  roleTable: FormGroup;
  roleScreenControls: FormArray;
  
  lang: string = 'en';
  langServiceSupscribtion: Subscription | undefined;
  Update = 0;
  faTrash = faTrash;
  faPen = faPenToSquare;
  faArrowAltCircleLeft = faArrowAltCircleLeft;



  addRow(roleScreen: InternalRoleScreen, index: number) {
    this.getFormControls.insert(index, this.initiateForm(roleScreen));
  }
  get getFormControls() {
    const controls = this.roleTable.get('tableRows') as FormArray;
    return controls;
  }
  initiateForm(screen: InternalRoleScreen): FormGroup {
    return this.fb.group({
      screenRoleId: new FormControl(screen.screenRoleId),
      screenOrder: new FormControl(screen.screenOrder),
      insert: new FormControl(screen.insert),
      update: new FormControl(screen.update),
      delete: new FormControl(screen.delete),
      query: new FormControl(screen.query),
      screenId: new FormControl(screen.screenId),
      hashedScreenId: new FormControl(screen.id),
      roleIdValue: new FormControl(this.ParamId),
      screenNameAr: new FormControl(screen.screenNameAr),
      screenNameEn: new FormControl(screen.screenNameEn),
    });
  }
  assignFormData() {
    console.log(this.dataList)
    this.dataList.forEach((roleScreen, index) => {
      this.addRow(roleScreen, index);
    });
  }

  GotoSCreenFieldPrivileges(screenId: string) {
    this.router.navigate(['screen-field-privilege'], { queryParams: { roleId: this.ParamId, screenId: screenId } });
  }


  constructor(
    @Inject(forwardRef(() => AppComponent)) private app: AppComponent,
    private translateService: TranslateService,
    private fb: FormBuilder,
    private commonServices: CommonServices,
    private roleServices: RoleService,
    private userService: UserInfoServices,
    private router: Router,
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef,
    private sharedService: SharedService,
    private commonClass: CommonClass,
    private titleService: Title) {
    this.route.queryParams.subscribe(params => {
      this.ParamId = params["Id"];
    });
  }

  ngOnInit(): void {
    this.roleTable = this.fb.group({
      tableRows: this.fb.array([]),
    });
    this.roleScreenControls = this.getFormControls;
    this.getUserData();
 
    this.langServiceSupscribtion = this.sharedService.lang.subscribe(
      (l) => (this.lang = l)
    );
  }
  ngAfterViewInit(): void {
    this.translateService.stream('ApplicationTitle').subscribe(value => {
      this.translateService.stream('ScreenFields.PageTitle').subscribe(valueTitle => {
        this.titleService.setTitle(value + valueTitle);
      });
    });
  }


  getUserData(): void {
    var _this = this;

    _this.currentUser=_this.userService.getCurrentUser();
    _this.getData();
  }
  getData() {
    var _this = this;


    _this.roleServices.GetAllScreens(_this.ParamId).subscribe(function (data) {
      var response = data as Response<InternalRoleScreen[]>;
      if (response.succeeded) {
        $('#datatableexample').DataTable().destroy();
        _this.dataList = [];
        _this.dataList = response.data;
        _this.assignFormData();
        setTimeout(() => {
          $('#datatableexample').DataTable({
            pagingType: 'full_numbers',
            pageLength: 5,
            processing: true,
            lengthMenu: [5, 10, 25]
          });
        }, 1);
      }
      else {
        _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
      }
    })
  }


  reset() {
    this.Update = 0;
    this.roleTable.reset();
  }

  save() {
    var _this = this;

    let data = _this.commonClass.onSubmitEditedProfile(_this.getFormControls).map(xx => {

      return <InternalRoleScreen>
        {

        id: xx.id,
        insert: xx.insert,
        update: xx.update,
        delete: xx.delete,
        query: xx.query,
        screenId: xx.screenId,
        roleIdValue: this.ParamId,
        screenOrder: xx.screenOrder,
        screenRoleId: xx.screenRoleId
        };

    });

 
    _this.commonClass.onSubmitEditedProfile(_this.getFormControls) 
    console.log(_this.commonClass.onSubmitEditedProfile(_this.getFormControls))
    _this.roleServices.AddRoleScreens(data).subscribe(function (data) {
        var response = data as Response<boolean>;
        if (response.succeeded && response.data) {
          // _this.getData();
          location.reload();
          _this.app.messageService.add({ severity: 'success', key: 'PlanValidation', summary: _this.translateService.instant('General.SavedSuccessfully'), detail: _this.translateService.instant('General.SavedSuccessfully'), life: 6000 });
        } else {
          _this.app.messageService.add({ severity: 'error', key: 'PlanValidation', summary: _this.translateService.instant('General.Error'), detail: _this.translateService.instant('General.Error') + " " + response.errors.join('=>'), life: 6000 });
        }

      });
  }
  Back() {
    this.router.navigate(['roles']);
  }
 

  isNumberKey(evt: any) {
    return this.commonClass.isNumberKey(evt);
  }
  OnlyArabic(event: any, lng: any) {
    return this.commonClass.OnlyArabic(event, lng);
  }
  OnlyEnglish(event: any, lng: any) {
    return this.commonClass.OnlyEnglish(event, lng);
  }
 
}
