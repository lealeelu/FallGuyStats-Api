import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Config } from '../models/config.model';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  private config: Config
  public configSubject$ = new BehaviorSubject<Config>(new Config())

  constructor(private http: HttpClient) {
  }

  updateConfig(config: Config) {
    this.config = config
    this.configSubject$.next(this.config)
  }

  loadConfig() {
    let config = new Config();
    config.apiUrl = "http://localhost:5000/api/Stats"
    config.pollingFrequency = 10000
    config.showLastEpisode = false
    config.showCheaterCount = false
    config.showLosingStreak = true
    config.showCredits = true
    this.config = config
    this.configSubject$.next(this.config)
    //this doesn't work if you are running directly from file system
    //instead of webserver - rework to use db
    /*
    let headers = new HttpHeaders()
      .set('Content-type', 'application/json')
      .set('Access-Control-Allow-Origin', '*')
      .set('Accept', 'applcation/json')
     
    return this.http
      .get<Config>('assets/config.json', { 'headers': headers })
      .toPromise()
      .then(config => {
        this.config = config
        this.configSubject$.next(this.config)
      });
      */
  }
}