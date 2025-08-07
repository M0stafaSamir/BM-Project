import { AuthService } from './../../../core/services/auth.service';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GetJob } from '../../models/job.model';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-job-card',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './job-card.component.html',
  styleUrl: './job-card.component.css',
})
export class JobCardComponent {
  @Input() job!: GetJob;
  @Output() delete = new EventEmitter<string>();

  onDelete() {
    this.delete.emit(this.job.id);
  }
}
