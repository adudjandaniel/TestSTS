import { HttpClient } from '@angular/common/http';
import { HttpTestingController, HttpClientTestingModule } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { AuthorizeService } from './authorize.service';

describe('Authorize', () => {
  let httpClient: HttpClient;
  
  beforeEach(() => {
    TestBed.configureTestingModule({
        imports: [ HttpClientTestingModule ]
    });
    
    httpClient = TestBed.get(HttpClient);
  });

  it('should create an instance', () => {
    expect(new AuthorizeService(httpClient)).toBeTruthy();
  });
});
