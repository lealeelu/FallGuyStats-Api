import { BrowserModule } from '@angular/platform-browser'
import { APP_INITIALIZER, NgModule } from '@angular/core'

import { AppComponent } from './app.component'
import { StatsComponent } from './stats/stats.component'
import { HttpClientModule } from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module'
import { AdminComponent } from './admin/admin.component'
import { ConfigService } from './services/config.service'
import { APP_BASE_HREF, LocationStrategy, HashLocationStrategy } from '@angular/common'
import { FormsModule } from '@angular/forms'

export const configFactory = (configService: ConfigService) => {
  return () => configService.loadConfig()
}

@NgModule({
  declarations: [
    AppComponent,
    StatsComponent,
    AdminComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule
  ],
  bootstrap: [AppComponent],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: configFactory,
      deps: [ConfigService],
      multi: true
    },
    // { provide: APP_BASE_HREF, useValue: './'},
    // { provide: LocationStrategy, useClass: HashLocationStrategy}
  ]
})
export class AppModule { }
