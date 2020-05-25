import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DebtListComponent } from './debt-list/debt-list.component';
import { DebtComponent } from './debt.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MaterialModule } from '@app/material.module';
import { DebtRoutingModule } from './debt-routing.module';

@NgModule({
  imports: [ CommonModule, FlexLayoutModule, MaterialModule, DebtRoutingModule ],
  declarations: [DebtComponent, DebtListComponent]
})
export class DebtModule { }
