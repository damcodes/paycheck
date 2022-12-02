import { AuthGuard } from './can-activate-auth-guard';

describe('CanActivateAuthGuard', () => {
  it('should create an instance', () => {
    expect(new AuthGuard()).toBeTruthy();
  });
});
