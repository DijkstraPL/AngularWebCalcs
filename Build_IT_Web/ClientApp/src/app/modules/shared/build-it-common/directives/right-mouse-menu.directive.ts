import {
  Directive,
  ElementRef,
  HostListener,
  Input,
  OnInit,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';

@Directive({
  selector: '[appRightMouseMenu]',
})
export class RightMouseMenuDirective {
  @Input()
  menuData: any;
  @Input()
  menu: any;
  constructor(private menuTrigger: MatMenuTrigger) {
  }

  @HostListener('contextmenu', ['$event'])
  onRightClick(event: MouseEvent) {
    event.preventDefault();
    this.menuTrigger.menuData = this.menuData;
    this.menu.data = this.menuData;
    this.menuTrigger.openMenu();
  }

  @HostListener('click', ['$event'])
  onLeftClick(event: MouseEvent) {
    event.preventDefault();
    this.menuTrigger.closeMenu();
  }
}
