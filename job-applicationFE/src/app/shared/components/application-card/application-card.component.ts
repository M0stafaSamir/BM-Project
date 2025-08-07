import { Component, Input } from '@angular/core';
import { GetApplication } from '../../models/application.model';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-application-card',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './application-card.component.html',
  styleUrl: './application-card.component.css',
})
export class ApplicationCardComponent {
  @Input() application!: GetApplication;

  @Input() deleteApp!: (id: number) => void;
}
