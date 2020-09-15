import { Component, OnInit } from '@angular/core';
import { Config } from '../models/config.model';
import { ConfigService } from '../services/config.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.sass']
})
export class AdminComponent implements OnInit {

  showLosingStreak: boolean
  showCredits: boolean
  showCheaterCount: boolean

  constructor(
    private configService: ConfigService
  ) { }

  ngOnInit(): void {
  }

  updateConfig(event) {
    var config = new Config()
    config.showLosingStreak = this.showLosingStreak
    config.showCredits = this.showCredits
    config.showCheaterCount = this.showCheaterCount
    this.configService.updateConfig(config)    
  }

}
