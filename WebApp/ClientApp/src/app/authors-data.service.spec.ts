import { TestBed, inject } from '@angular/core/testing';

import { AuthorsDataService } from './authors-data.service';

describe('AuthorsDataService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [AuthorsDataService]
        });
    });

    it('should be created', inject([AuthorsDataService], (service: AuthorsDataService) => {
        expect(service).toBeTruthy();
    }));
});
