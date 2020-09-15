import { Injectable, ɵConsole } from '@angular/core';
import { Observable, timer, Subject } from 'rxjs';
import { StatResponse } from '../models/stat-response.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { tap, switchMap, retry, share, takeUntil } from 'rxjs/operators';
import { ConfigService } from './config.service';

@Injectable({
  providedIn: 'root'
})
export class StatService {

  private stats$: Observable<StatResponse>
  private stopPolling = new Subject()

  constructor(
    private http: HttpClient,
    private configService: ConfigService
  ) { 
    let headers = new HttpHeaders()
      .set('Content-type', 'application/json')
      .set('Access-Control-Allow-Origin', '*')
      .set('Accept', 'applcation/json')
    this.stats$ = timer(1, configService.configSubject$.value.pollingFrequency).pipe(
      switchMap(() => this.http.get<StatResponse>(configService.configSubject$.value.apiUrl, { 'headers': headers })),
      retry(),
      share(),
      takeUntil(this.stopPolling))
  }

  getStats(): Observable<StatResponse> {
    return this.stats$.pipe(
      tap((stats) => {
        console.log('api tapped')
      })
    )
  }

  ngOnDestroy() {
    this.stopPolling.next()
  }
}
