import { EnumAsStringPipe } from './enum-as-string.pipe';

describe('EnumAsStringPipe', () => {
  it('create an instance', () => {
    const pipe = new EnumAsStringPipe();
    expect(pipe).toBeTruthy();
  });
});
