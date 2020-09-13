import { Component, OnInit } from '@angular/core'
import { StatResponse, RoundStats } from '../models/stat-response.model'
import { Observable} from 'rxjs';
import { StatService } from '../services/stat.service'
import { tap, distinctUntilChanged } from 'rxjs/operators';
import { ConfigService } from '../services/config.service';

@Component({
  selector: 'app-stats',
  templateUrl: './stats.component.html',
  styleUrls: ['./stats.component.sass']
})
export class StatsComponent implements OnInit {

  stats$: Observable<StatResponse>
  todayWinrate: string
  seasonWinrate: string

  showLastEpisode: boolean
  showCheaterCount: boolean
  showLosingStreak: boolean 
  showCredits: boolean

  constructor(
    private statService: StatService,
    private configService: ConfigService
  ) {
    configService.configSubject$
    .pipe(distinctUntilChanged())
    .subscribe(config => {
      this.showCheaterCount = config.showCheaterCount
      this.showLastEpisode = config.showLastEpisode
      this.showLosingStreak = config.showLosingStreak
      this.showCredits = config.showCredits
    })
  }

  ngOnInit(): void {
    this.stats$ = this.statService.getStats().pipe(
      tap((stats: StatResponse) => {
        //episode
        if (stats.todayStats.episodeCount > 0)
          this.todayWinrate = (stats.todayStats.crownCount/stats.todayStats.episodeCount * 100).toFixed(2)
        else
          this.todayWinrate = '0'
        if (stats.seasonStats.episodeCount > 0)
          this.seasonWinrate = (stats.seasonStats.crownCount/stats.seasonStats.episodeCount * 100).toFixed(2)
        else this.seasonWinrate = '0'
        if (stats.roundStats && stats.roundStats.qualifiedCount > 0) {
          stats.roundStats.qualifiedCount -= stats.roundStats.goldCount
            + stats.roundStats.silverCount + stats.roundStats.bronzeCount
        }
      }));
  }
}
