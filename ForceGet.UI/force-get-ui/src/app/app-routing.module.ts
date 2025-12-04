// app-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { QuoteFormComponent } from './quote/form/quote-form.component';
import { QuoteListComponent } from './quote/list/quote-list.component';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: '/quotes/form', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: 'quotes/form',
    component: QuoteFormComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'quotes/list',
    component: QuoteListComponent,
    canActivate: [AuthGuard],
  },
  { path: '**', redirectTo: '/quotes/form' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
