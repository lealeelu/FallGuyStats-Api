import { Component, OnInit, OnDestroy } from '@angular/core'
import { Config } from '../models/config.model'
import { ConfigService } from '../services/config.service'
import { Subject } from 'rxjs'
import { takeUntil } from 'rxjs/operators'


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.sass']
})
export class AdminComponent implements OnInit, OnDestroy {

  config: Config
  private destroyed$ = new Subject()

  constructor(
    private configService: ConfigService
  ) {
  }

  ngOnInit(): void {
    this.configService.configSubject$.pipe(takeUntil(this.destroyed$)).subscribe((config) => {
      this.config = config
    })
  }

  saveConfig(): void {
    this.configService.updateConfig(this.config)
  }

  ngOnDestroy(): void {
    this.destroyed$.next()
  }

}
