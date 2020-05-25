import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DebtComponent } from './debt.component';
import { DebtListComponent } from './debt-list/debt-list.component';

const routes: Routes = [
  // Module is lazy loaded, see app-routing.module.ts
  { path: '', component: DebtComponent },
  {path: 'list', component: DebtListComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class DebtRoutingModule { }
