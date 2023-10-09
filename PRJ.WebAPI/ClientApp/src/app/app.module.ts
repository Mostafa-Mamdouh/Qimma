import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import {
  HttpClient,
  HttpClientModule,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MainContainerComponent } from './components/layout/main-container/main-container.component';
import { NavbarComponent } from './components/layout/navbar/navbar.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { SidebarComponent } from './components/layout/sidebar/sidebar/sidebar.component';
import { SidebarCategoryItemComponent } from './components/layout/sidebar/sidebar-category-item/sidebar-category-item.component';
import { SidebarItemComponent } from './components/layout/sidebar/sidebar-item/sidebar-item.component';
import { SidebarSearchComponent } from './components/layout/sidebar/sidebar-search/sidebar-search.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { QuickActionComponent } from './components/quick-action/quick-action.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MainMenuItemComponent } from './components/main-menu-item/main-menu-item.component';
import { MainMenuComponent } from './Pages/main-menu/main-menu.component';
import { Helper } from 'src/app/helper/Helper';
import { UserDetailsComponent } from './components/common/user-details/user-details.component';
import { InitCapPipe } from './Pipes/initCap/init-cap.pipe';
import { SharedService } from 'src/app/services/Shared/shared.service';
import { PdfViewerModule } from 'ng2-pdf-viewer';
import { NgxDropzoneModule } from 'ngx-dropzone';
import { DataTablesModule } from 'angular-datatables';
import { ToastrModule } from 'ngx-toastr';
import { ToastModule } from 'primeng/toast';
import { MatTableModule } from '@angular/material/table';
import { AutoCompleteModule } from 'primeng/autocomplete';
import {
  MatPaginatorIntl,
  MatPaginatorModule,
} from '@angular/material/paginator';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { DialogModule } from 'primeng/dialog';
import { TableModule } from 'primeng/table';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CookieService } from 'ngx-cookie-service';
import { AddNewSealedSourceComponent } from './components/facilities/add-new-sealed-source/add-new-sealed-source.component';
import { AddNewUnsealedSourceComponent } from './components/facilities/add-new-unsealed-source/add-new-unsealed-source.component';
import { AddNewVariableunsealedSourceComponent } from './components/facilities/add-new-variableunsealed-source/add-new-variableunsealed-source.component';
import { MessageService, ConfirmationService } from 'primeng/api';
import { LoaderInterceptorService } from './services/General/loader-interceptor.service';
import { APP_BASE_HREF } from '@angular/common';
import { LoaderComponent } from './components/layout/loader/loader.component';
import { TestDatatableComponent } from './components/test-datatable/test-datatable.component';
import { MainComponent } from './components/main/main.component';
import { FooterComponent } from './components/layout/Footer/Footer.component';
import { AddNewGeneratorComponent } from './components/facilities/add-new-generator/add-new-generator.component';
import { NgApexchartsModule } from 'ng-apexcharts';
import { WorkerComponent } from './components/Worker/worker.component';
import { DropdownModule } from 'primeng/dropdown';
import { InputSwitchModule } from 'primeng/inputswitch';
import { SealedTransactionEditComponent } from './components/facilities/sealed-transaction-edit/sealed-transaction-edit.component';
import { UnsealedTransactionEditComponent } from './components/facilities/unsealed-transaction-edit/unsealed-transaction-edit.component';
import { VariableUnsealedTransactionEditComponent } from './components/facilities/variable-unsealed-transaction-edit/variable-unsealed-transaction-edit.component';
import { JwtModule } from '@auth0/angular-jwt';
import { ErrorInterceptor } from 'src/core/interceptors/error.interceptors';
import { AppSettings } from './AppSettings';
import { HasClaimDirective } from 'src/core/security/has-claim.directive';
import { LoginComponent } from './components/Account/Login/login.component';
import { JwtInterceptor } from 'src/core/interceptors/jwt.interceptor';
import { WorkerDetailsComponent } from './components/worker-details/worker-details.component';

export function tokenGetter() {
  return localStorage.getItem('JWT_TOKEN');
}
import { BillingTrnServiceComponent } from './components/billing-trn-service/billing-trn-service.component';
import { InsertBillingTrnServiceComponent } from './components/billing-trn-service/insert-billing-trn-service/insert-billing-trn-service.component';
import { WorkerMassUpdateComponent } from './components/worker-mass-update/worker-mass-update.component';
import { AddNewWorkerComponent } from './components/add-new-worker/add-new-worker.component';
import { ListboxModule } from 'primeng/listbox';
import { ScrollTopModule } from 'primeng/scrolltop';

@NgModule({
  declarations: [
    AppComponent,
    MainContainerComponent,
    NavbarComponent,
    SidebarComponent,
    SidebarCategoryItemComponent,
    SidebarItemComponent,
    SidebarSearchComponent,
    QuickActionComponent,
    MainMenuItemComponent,
    MainMenuComponent,
    UserDetailsComponent,
    InitCapPipe,
    AddNewSealedSourceComponent,
    AddNewUnsealedSourceComponent,
    AddNewVariableunsealedSourceComponent,
    LoaderComponent,
    TestDatatableComponent,
    MainComponent,
    FooterComponent,
    AddNewGeneratorComponent,
    WorkerComponent,
    SealedTransactionEditComponent,
    UnsealedTransactionEditComponent,
    VariableUnsealedTransactionEditComponent,
    HasClaimDirective,
    LoginComponent,
    WorkerDetailsComponent,
    BillingTrnServiceComponent,
    InsertBillingTrnServiceComponent,
    WorkerMassUpdateComponent,
    AddNewWorkerComponent,
  ],
  imports: [
    BrowserModule,
    PdfViewerModule,
    AppRoutingModule,
    HttpClientModule,
    NgxDropzoneModule,
    DataTablesModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: httpTranslateLoader,
        deps: [HttpClient],
      },
    }),
    NgbModule,
    ToastModule,
    FontAwesomeModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatSortModule,
    MatInputModule,
    MatProgressSpinnerModule,
    NgApexchartsModule,
    AutoCompleteModule,
    DropdownModule,
    InputSwitchModule,
    ScrollTopModule,
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
    ConfirmDialogModule,
    TableModule,
    DynamicDialogModule,
    DialogModule,
    ListboxModule,
  ],
  providers: [
    { provide: APP_BASE_HREF, useValue: '/' },
    Helper,
    SharedService,
    MessageService,
    ConfirmationService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoaderInterceptorService,
      multi: true,
    },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    CookieService,
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule {}

export function httpTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
