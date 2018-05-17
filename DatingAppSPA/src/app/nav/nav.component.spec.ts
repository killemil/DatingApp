import { TestBed, inject } from '@angular/core/testing';

import { NavComponent } from './nav.component';

describe('a nav component', () => {
  let component: NavComponent;

  // register all needed dependencies
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [NavComponent]
    });
  });

  // instantiation through framework injection
  beforeEach(
    inject([NavComponent], NavComponent => {
      component = NavComponent;
    })
  );

  it('should have an instance', () => {
    expect(component).toBeDefined();
  });
});
