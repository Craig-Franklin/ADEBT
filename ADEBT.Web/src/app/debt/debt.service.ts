import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Debt } from './debt';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '@env/environment';
import { catchError } from 'rxjs/operators';
import { CredentialsService } from '@app/core';

@Injectable({
  providedIn: 'root'
})
export class DebtService {

  constructor(
    private http: HttpClient
  ) { }

  list(): Observable<Debt[]>{
    return this.http.get<Debt[]>(`${environment.serverUrl}/debts`);
  }
}
