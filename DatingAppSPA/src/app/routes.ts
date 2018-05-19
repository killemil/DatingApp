import { AuthGuard } from './_guards/auth.guard';
import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './member-list/member-list.component';
import { MessageBundle } from '@angular/compiler';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';

export const routes: Routes = [
    { path: 'home', component: HomeComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'members', component: MemberListComponent },
            { path: 'messages', component: MessagesComponent },
            { path: 'lists', component: ListsComponent },
        ]
    },
    { path: '**', redirectTo : 'home', pathMatch: 'full' }
];
