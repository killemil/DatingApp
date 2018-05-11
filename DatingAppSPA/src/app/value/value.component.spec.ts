import { TestBed, inject } from '@angular/core/testing';

import { ValueComponent } from './value.component';

describe('a value component', () => {
	let component: ValueComponent;

	// register all needed dependencies
	beforeEach(() => {
		TestBed.configureTestingModule({
			providers: [
				ValueComponent
			]
		});
	});

	// instantiation through framework injection
	beforeEach(inject([ValueComponent], (ValueComponent) => {
		component = ValueComponent;
	}));

	it('should have an instance', () => {
		expect(component).toBeDefined();
	});
});