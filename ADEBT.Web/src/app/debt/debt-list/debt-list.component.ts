import { Component, OnInit } from '@angular/core';
import { Debt } from '../debt';
import { DebtService } from '../debt.service';
import { finalize } from 'rxjs/operators';
import { untilDestroyed } from '@app/core';

@Component({
  selector: 'app-debt-list',
  templateUrl: './debt-list.component.html',
  styleUrls: ['./debt-list.component.scss']
})
export class DebtListComponent implements OnInit {
  debts: Debt[];
  isLoading: boolean;

  constructor(private debtService: DebtService) { }

  ngOnInit() {
    this.isLoading = true;
    this.debtService
      .list()
      .pipe(
        finalize(() => {
          this.isLoading = false;
        })
      )
      .subscribe((debts: Debt[]) => {
        this.debts = debts;
      });
  }
}
