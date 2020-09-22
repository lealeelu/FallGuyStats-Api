import { Injectable, ɵConsole, OnDestroy } from '@angular/core'
import { Observable, timer, Subject } from 'rxjs'
import { StatResponse } from '../models/stat-response.model'
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { tap, switchMap, retry, share, takeUntil } from 'rxjs/operators'
import { ConfigService } from './config.service'
import { environment } from '../../environments/environment'

@Injectable({
  providedIn: 'root'
})
export class StatService implements OnDestroy {

  private stats$: Observable<StatResponse>
  private stopPolling = new Subject()

  constructor(
    private http: HttpClient,
    private configService: ConfigService
  ) {
    const httpHeaders = new HttpHeaders()
      .set('Content-type', 'application/json')
      .set('Access-Control-Allow-Origin', '*')
      .set('Accept', 'applcation/json')
    this.stats$ = timer(1, configService.configSubject$.value.pollingFrequency).pipe(
      switchMap(() => this.http.get<StatResponse>(environment.settingAPI, { headers: httpHeaders })),
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

  ngOnDestroy(): void {
    this.stopPolling.next()
  }
}
