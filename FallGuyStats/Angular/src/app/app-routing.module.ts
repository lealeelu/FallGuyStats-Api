import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin/admin.component'
import { RouterModule, Routes } from '@angular/router';
import { StatsComponent } from './stats/stats.component';

const routes: Routes = [
  { path: 'admin', component: AdminComponent },
  { path: 'stats', component: StatsComponent },
  { path: '', redirectTo: '/stats', pathMatch: 'full' }
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
