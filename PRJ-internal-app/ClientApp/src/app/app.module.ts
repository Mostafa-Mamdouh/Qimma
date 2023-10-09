import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  HttpClient,
  HttpClientModule,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import {
  TranslateLoader,
  TranslateModule,
  TranslateService,
} from '@ngx-translate/core';
import { APP_BASE_HREF } from '@angular/common';
import { MyMissingTranslationHandler } from '../MyMissingTranslationHandler';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CustomTranslateLoader } from '../CustomTranslateLoader';
import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { FieldsetModule } from 'primeng/fieldset';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from 'primeng/dialog';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { ToastModule } from 'primeng/toast';
import { ChartModule } from 'primeng/chart';
import { RadioButtonModule } from 'primeng/radiobutton';
import { FileUploadModule } from 'primeng/fileupload';
import { InputMaskModule } from 'primeng/inputmask';
import { NgxMaskModule, IConfig } from 'ngx-mask';
const maskConfig: Partial<IConfig> = {
  validation: false,
};
import { DataTablesModule } from 'angular-datatables';
import { TreeTableModule } from 'primeng/treetable';
import { ContextMenuModule } from 'primeng/contextmenu';

import { MainMenuItemComponent } from './components/main-menu-item/main-menu-item.component';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MainContainerComponent } from './components/layout/main-container/main-container.component';
import { NavbarComponent } from './components/layout/navbar/navbar.component';
import { SidebarComponent } from './components/layout/sidebar/sidebar/sidebar.component';
import { SidebarCategoryItemComponent } from './components/layout/sidebar/sidebar-category-item/sidebar-category-item.component';
import { SidebarItemComponent } from './components/layout/sidebar/sidebar-item/sidebar-item.component';
import { SidebarSearchComponent } from './components/layout/sidebar/sidebar-search/sidebar-search.component';

import { MatTableModule } from '@angular/material/table';
import {
  MatPaginatorIntl,
  MatPaginatorModule,
} from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSortModule } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { DatatableComponent } from './components/common/datatable/datatable.component';
import { MatPaginatorIntlCro } from './components/common/datatable/MatPaginatorIntlCro';

import { UserInfoServices } from '../services/userinfo.service';
import { UserInfoComponent } from '../UserInfo/user-info.component';
import { Helper } from '../helper/helper';
import { LookupSetComponent } from './components/LookupSet/lookupset.component';
import { LookupSetTermComponent } from './components/LookupSet/LookupSetTerm/lookupset-term.component';
import { ConfirmationService, MessageService } from 'primeng/api';
import { LoaderService } from '../services/General/loader.service';
import { LoaderComponent } from './components/layout/loader/loader.component';
import { BasCountriesComponent } from './components/BasCountries/bascountries.component';
import { BasCitesComponent } from './components/BasCites/bascites.component';
import { ManufacturerMasterComponent } from './components/ManufacturerMaster/manufacturermaster.component';
import { RadionucliedeSetupComponent } from './components/radionucliede-setup/radionucliede-setup.component';
import { LoaderInterceptorService } from '../services/General/loader-interceptor.service';
import { CommonServices } from '../services/Common/common.service';
import { PractiseProfileComponent } from './components/PractiseProfile/practise-profile.component';

import { EntityComponent } from './components/Entity/Entity.component';
import { FacilityComponent } from './components/Facility/Facility.component';
import { LicenseComponent } from './components/License/License.component';
import { LegalRepresentativesProfileComponent } from './components/LegalRepresentativesProfile/LegalRepresentativesProfile.component';
import { SafetyResponsibleOfficerComponent } from './components/SafetyResponsibleOfficer/SafetyResponsibleOfficer.component';
import { SystemSettingsComponent } from './components/SystemSettings/system-settings';
import { AmanSettingsComponent } from './components/AmmanSettings/aman-settings';
import { ClipboardModule } from 'ngx-clipboard';
import { ScreenComponent } from './components/ScreenManagement/screen.component';
import { LovComponent } from './components/LovManagement/lov.component';
import { RoleComponent } from './components/InternalRole/role.component';
import { InternalUserSettingsComponent } from './components/InternalUserSettings/internal-user-settings';
import { WorkersComponent } from './components/Workers/Workers.component';
import { WorkersServices } from '../services/Workers/Workers.service';
import { ChangeFacilityIdComponent } from './components/ActionCenter/change-facility-id/change-facility-id.component';
import { NewSourceFromImportComponent } from './components/ActionCenter/new-source-from-import/new-source-from-import.component';
import { BillingComponent } from './components/Billing-Demo/billing/billing.component';
import { ScreenFieldComponent } from './components/ScreenManagement/ScreenFields/screen-field.component';
import { InternalRoleScreenComponent } from './components/InternalRole/RoleScreens/internal-role-screen.component';
import { FieldPermissionComponent } from './components/ScreenManagement/FieldPermissions/field-permission.component';
import { SelectAreaComponent } from './components/common/select-area/select-area.component';
import { ActionCenterComponent } from './components/ActionCenter/ActionCenter/action-center.component';
import { TransactionEnquiryComponent } from './components/EnquiryScreen/trn-enquiry.component';
import { DropdownModule } from 'primeng/dropdown';
import { MultiSelectModule } from 'primeng/multiselect';
import { LoginComponent } from './components/Account/Login/login.component';
import { AuthGuard } from 'src/core/guards/auth.guard';
import { AppSettings } from 'src/AppSettings';
import { ErrorInterceptor } from 'src/core/interceptors/error.interceptors';
import { ToastrModule } from 'ngx-toastr';
import { HasClaimDirective } from 'src/core/security/has-claim.directive';
import { JwtModule } from '@auth0/angular-jwt';
import { JwtInterceptor } from 'src/core/interceptors/jwt.interceptor';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { NuclearMaterialFormComponent } from './components/nuclear-material/nuclear-material-form/nuclear-material-form.component';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { InputSwitchModule } from 'primeng/inputswitch';
import { NuclearMaterialsComponent } from './components/nuclear-material/nuclear-materials/nuclear-materials.component';
import { RelateditemsComponent } from './components/relateditems/relateditems.component';
import { CustomerprofileComponent } from './components/customerprofile/customerprofile.component';
import { BillingTrnServiceComponent } from './components/billing-trn-service/billing-trn-service.component';
import { InsertBillingTrnServiceComponent } from './components/billing-trn-service/insert-billing-trn-service/insert-billing-trn-service.component';

export function tokenGetter() {
  return localStorage.getItem('JWT_TOKEN');
}
import { RelatedItemComponent } from './components/RelatedItem/RelatedItem.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ServerErrorComponent } from './components/server-error/server-error.component';
import { RelatedItemsTreeComponent } from './components/relateditems/related-items-tree/related-items-tree.component';
import { RelatedItemsSetupComponent } from './components/relateditems/related-items-setup/related-items-setup.component';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    UserInfoComponent,
    LookupSetComponent,
    LookupSetTermComponent,
    LoaderComponent,
    MainMenuItemComponent,
    AppComponent,
    MainContainerComponent,
    NavbarComponent,
    SidebarComponent,
    SidebarCategoryItemComponent,
    SidebarItemComponent,
    SidebarSearchComponent,
    BasCountriesComponent,
    BasCitesComponent,
    ManufacturerMasterComponent,
    RadionucliedeSetupComponent,
    PractiseProfileComponent,
    DatatableComponent,
    EntityComponent,
    FacilityComponent,
    LicenseComponent,
    LegalRepresentativesProfileComponent,
    SafetyResponsibleOfficerComponent,
    SystemSettingsComponent,
    AmanSettingsComponent,
    ScreenComponent,
    LovComponent,
    RoleComponent,
    AmanSettingsComponent,
    WorkersComponent,
    ChangeFacilityIdComponent,
    NewSourceFromImportComponent,
    BillingComponent,
    InternalUserSettingsComponent,
    ScreenFieldComponent,
    InternalRoleScreenComponent,
    FieldPermissionComponent,
    SelectAreaComponent,
    ActionCenterComponent,
    TransactionEnquiryComponent,
    LoginComponent,
    HasClaimDirective,
    NuclearMaterialFormComponent,
    NuclearMaterialsComponent,
    BillingComponent,
    RelatedItemComponent,
    RelateditemsComponent,
    CustomerprofileComponent,
    BillingTrnServiceComponent,
    InsertBillingTrnServiceComponent,
    NotFoundComponent,
    ServerErrorComponent,
    RelatedItemsTreeComponent,
    RelatedItemsSetupComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    DataTablesModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: httpTranslateLoader,
        deps: [HttpClient],
      },
    }),
    FieldsetModule,
    CardModule,
    TableModule,
    ToolbarModule,
    ButtonModule,
    RippleModule,
    InputTextModule,
    DialogModule,
    InputTextareaModule,
    ConfirmDialogModule,
    MessagesModule,
    MessageModule,
    ToastModule,
    ChartModule,
    RadioButtonModule,
    FileUploadModule,
    InputMaskModule,
    NgxMaskModule.forRoot(maskConfig),
    FormsModule,
    FontAwesomeModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatSortModule,
    MatInputModule,
    MatProgressSpinnerModule,
    ClipboardModule,
    TreeTableModule,
    ContextMenuModule,
    DropdownModule,
    AutoCompleteModule,
    MultiSelectModule,
    NgxDropzoneModule,
    DynamicDialogModule,
    NgxDropzoneModule,
    InputSwitchModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: [AppSettings.WebApiUrl],
        disallowedRoutes: [],
      },
    }),
    ToastrModule.forRoot({
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    }),
    RouterModule.forRoot([
      {
        path: '',
        component: HomeComponent,
        canActivate: [AuthGuard],
        pathMatch: 'full',
      },
      {
        path: 'system-settings',
        component: SystemSettingsComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'aman-settings',
        component: AmanSettingsComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'counter',
        component: CounterComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'fetch-data',
        component: FetchDataComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'user-info',
        component: UserInfoComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'lookup-set',
        component: LookupSetComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'lookup-set-term',
        component: LookupSetTermComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'bas-countries',
        component: BasCountriesComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'bas-cites',
        component: BasCitesComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'manufacturer-master',
        component: ManufacturerMasterComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'radionuclide-setup',
        component: RadionucliedeSetupComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'practise-profile',
        component: PractiseProfileComponent,
        canActivate: [AuthGuard],
      },
      { path: 'entity', component: EntityComponent, canActivate: [AuthGuard] },
      {
        path: 'facility',
        component: FacilityComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'license',
        component: LicenseComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'legal',
        component: LegalRepresentativesProfileComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'safety',
        component: SafetyResponsibleOfficerComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'workers',
        component: WorkersComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'safety',
        component: SafetyResponsibleOfficerComponent,
        canActivate: [AuthGuard],
      },
      { path: 'screens', component: ScreenComponent, canActivate: [AuthGuard] },
      { path: 'lov', component: LovComponent, canActivate: [AuthGuard] },
      { path: 'roles', component: RoleComponent, canActivate: [AuthGuard] },
      {
        path: 'internal-user-settings',
        component: InternalUserSettingsComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'screens-fields',
        component: ScreenFieldComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'role-screens',
        component: InternalRoleScreenComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'screen-field-privilege',
        component: FieldPermissionComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'action-center-change-facility-id',
        component: ChangeFacilityIdComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'action-center-new-source-from-import',
        component: NewSourceFromImportComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'billing',
        component: BillingComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'action-center',
        component: ActionCenterComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'trn-enquiry',
        component: TransactionEnquiryComponent,
        canActivate: [AuthGuard],
      },
      { path: 'login', component: LoginComponent },
      {
        path: 'nuclear-material-form',
        component: NuclearMaterialFormComponent,
      },
      { path: 'nuclear-materials', component: NuclearMaterialsComponent },
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'system-settings', component: SystemSettingsComponent },
      { path: 'aman-settings', component: AmanSettingsComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'user-info', component: UserInfoComponent },
      { path: 'lookup-set', component: LookupSetComponent },
      { path: 'lookup-set-term', component: LookupSetTermComponent },
      { path: 'bas-countries', component: BasCountriesComponent },
      { path: 'bas-cites', component: BasCitesComponent },
      { path: 'manufacturer-master', component: ManufacturerMasterComponent },
      { path: 'radionuclide-setup', component: RadionucliedeSetupComponent },
      { path: 'practise-profile', component: PractiseProfileComponent },
      { path: 'entity', component: EntityComponent },
      { path: 'facility', component: FacilityComponent },
      { path: 'license', component: LicenseComponent },
      { path: 'legal', component: LegalRepresentativesProfileComponent },
      { path: 'safety', component: SafetyResponsibleOfficerComponent },
      { path: 'workers', component: WorkersComponent },
      { path: 'safety', component: SafetyResponsibleOfficerComponent },
      { path: 'screens', component: ScreenComponent },
      { path: 'lov', component: LovComponent },
      { path: 'roles', component: RoleComponent },
      {
        path: 'internal-user-settings',
        component: InternalUserSettingsComponent,
      },
      { path: 'server-error', component: ServerErrorComponent },
      { path: 'not-found', component: NotFoundComponent },
      {
        path: 'action-center-change-facility-id',
        component: ChangeFacilityIdComponent,
      },
      {
        path: 'action-center-new-source-from-import',
        component: NewSourceFromImportComponent,
      },
      { path: 'billing', component: BillingComponent },
      { path: 'menu-items-hierarchy', component: RelatedItemComponent },
      { path: 'customer-profile', component: CustomerprofileComponent },
      { path: 'related-items', component: RelateditemsComponent },
      { path: 'related-items-tree', component: RelatedItemsTreeComponent },
      { path: 'related-items-setup', component: RelatedItemsSetupComponent },
      {
        path: 'billing-trn-service',
        component: BillingTrnServiceComponent,
      },
      {
        path: 'InsertBillnigTrn',
        component: InsertBillingTrnServiceComponent,
      },
      {
        path: 'BillnigTrnEdit',
        component: InsertBillingTrnServiceComponent,
        data: { mode: 'Edit' },
      },
      {
        path: 'BillnigTrnView',
        component: InsertBillingTrnServiceComponent,
        data: { mode: 'View' },
      },
    ]),
  ],
  providers: [
    { provide: APP_BASE_HREF, useValue: '/' },
    TranslateService,
    {
      provide: MyMissingTranslationHandler,
      useClass: MyMissingTranslationHandler,
    },
    Helper,
    UserInfoServices,
    CommonServices,
    MessageService,
    WorkersServices,
    ConfirmationService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoaderInterceptorService,
      multi: true,
    },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    {
      provide: MatPaginatorIntl,
      useClass: MatPaginatorIntlCro,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

export function httpTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
