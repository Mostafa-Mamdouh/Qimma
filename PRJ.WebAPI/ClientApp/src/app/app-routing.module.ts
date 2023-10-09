import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainMenuComponent } from './Pages/main-menu/main-menu.component';
import { AddNewSealedSourceComponent } from './components/facilities/add-new-sealed-source/add-new-sealed-source.component';
import { AddNewUnsealedSourceComponent } from './components/facilities/add-new-unsealed-source/add-new-unsealed-source.component';
import { AddNewVariableunsealedSourceComponent } from './components/facilities/add-new-variableunsealed-source/add-new-variableunsealed-source.component';
import { TestDatatableComponent } from './components/test-datatable/test-datatable.component';
import { MainComponent } from './components/main/main.component';
import { AddNewGeneratorComponent } from './components/facilities/add-new-generator/add-new-generator.component';
import { WorkerComponent } from './components/Worker/worker.component';
import { AuthGuard } from 'src/core/guards/auth.guard';
import { LoginComponent } from './components/Account/Login/login.component';
import { SealedTransactionEditComponent } from './components/facilities/sealed-transaction-edit/sealed-transaction-edit.component';
import { VariableUnsealedTransactionEditComponent } from './components/facilities/variable-unsealed-transaction-edit/variable-unsealed-transaction-edit.component';
import { UnsealedTransactionEditComponent } from './components/facilities/unsealed-transaction-edit/unsealed-transaction-edit.component';
import { WorkerDetailsComponent } from './components/worker-details/worker-details.component';
import { BillingTrnServiceComponent } from './components/billing-trn-service/billing-trn-service.component';
import { InsertBillingTrnServiceComponent } from './components/billing-trn-service/insert-billing-trn-service/insert-billing-trn-service.component';
import { WorkerMassUpdateComponent } from './components/worker-mass-update/worker-mass-update.component';
import { AddNewWorkerComponent } from './components/add-new-worker/add-new-worker.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'main', component: MainMenuComponent, canActivate: [AuthGuard] },
  {
    path: 'add-sealed-source',
    component: AddNewSealedSourceComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'update-sealed-source',
    component: AddNewSealedSourceComponent,
  },
  {
    path: 'add-unsealed-source',
    component: AddNewUnsealedSourceComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'update-unsealed-source',
    component: AddNewUnsealedSourceComponent,
  },
  {
    path: 'add-variable-unsealed-source',
    component: AddNewVariableunsealedSourceComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'update-variable-unsealed-source',
    component: AddNewVariableunsealedSourceComponent,
  },
  {
    path: 'add-radiation-generator',
    component: AddNewGeneratorComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'test-datatable',
    component: TestDatatableComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'home',
    component: MainComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'worker',
    component: WorkerComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'workerDetails',
    component: WorkerDetailsComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'Addworker',
    component: AddNewWorkerComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'edit-sealed-transaction',
    component: SealedTransactionEditComponent,
  },
  {
    path: 'edit-unsealed-transaction',
    component: UnsealedTransactionEditComponent,
  },
  {
    path: 'edit-short-lived-unsealed-transaction',
    component: VariableUnsealedTransactionEditComponent,
  },
  {
    path: 'BillingTrnService',
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
  {
    path: 'worker-Mass-Update',
    component: WorkerMassUpdateComponent,
    data: { mode: 'View' },
  },
  { path: 'login', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
