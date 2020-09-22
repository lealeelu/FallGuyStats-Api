import { Injectable, OnDestroy } from '@angular/core'
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http'
import { Config } from '../models/config.model'
import { BehaviorSubject, Observable, Subject, EMPTY } from 'rxjs'
import { environment } from '../../environments/environment'
import { take, switchMap, map, catchError } from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  private config: Config
  public configSubject$ = new BehaviorSubject<Config>(new Config())

  constructor(
    private http: HttpClient,
  ) {
  }

  updateConfig(config: Config): void {
    this.config = config
    this.http.put(environment.configAPI, config).pipe(switchMap(
    (result) => {
      this.configSubject$.next(this.config)
      return EMPTY
    }),
    catchError((err: HttpErrorResponse)  => {
      if (err.status === 404) {
        // If we get a not found error it could mean that it couldn't find the record in the database
        // We can try instead to post to the API to create the record in the database
        return this.http.post(environment.configAPI.replace('/1', ''), config)
      } else {
        console.error('Could not put to API: ', err)
        return EMPTY
      }
    })).subscribe(
      (result) => {
        if (result) {
          this.configSubject$.next(this.config)
        }
      },
      (err) => console.error('Could not post to API', err)
    )
  }

  loadConfig(): void {
    const requestHeaders = new HttpHeaders()
    .set('Content-type', 'application/json')
    .set('Access-Control-Allow-Origin', '*')
    .set('Accept', 'applcation/json')
    this.http.get<Config>(environment.configAPI, { headers: requestHeaders }).pipe(take(1)).subscribe((storedConfig) =>
    {
      if (storedConfig?.id === 1) {
        this.config = storedConfig
      } else {
        const config = new Config()
        config.id = 1
        config.pollingFrequency = 10000
        config.showLastEpisode = false
        config.showCheaterCount = false
        config.showLosingStreak = true
        config.showCredits = true
        this.config = config
      }
      this.configSubject$.next(this.config)
    },
    (err) => {
      const config = new Config()
      config.id = 1
      config.pollingFrequency = 10000
      config.showLastEpisode = false
      config.showCheaterCount = false
      config.showLosingStreak = true
      config.showCredits = true
      this.config = config
      this.configSubject$.next(this.config)
    })
  }
}
