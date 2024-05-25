import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ThemeOption } from 'src/app/models/theme-option';
import { ThemeService } from 'src/app/services/theme.service';

@Component({
  selector: 'app-theme-menu',
  templateUrl: './theme-menu.component.html',
  styleUrls: ['./theme-menu.component.scss']
})
export class ThemeMenuComponent implements OnInit {
  @Input() public themeOptions: Array<ThemeOption> | null = [];
  @Output()public readonly themeChange: EventEmitter<string> = new EventEmitter<string>();

  constructor(private readonly themeService: ThemeService) { }

  ngOnInit(): void {
  }

  changeTheme(themeToSet: string) {
    this.themeChange.emit(themeToSet);
  }
}
