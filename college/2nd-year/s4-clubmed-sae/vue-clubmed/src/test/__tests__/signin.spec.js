import { describe, it, expect } from 'vitest';

import vuex from 'vuex';
import { shallowMount } from '@vue/test-utils';
import SignIn from '@/views/SignIn.vue';
import store from '@/store/index';

const cmp = shallowMount(SignIn, {
  global: {
    plugins: [vuex],
    mocks: {
      $store: store
    }
  }
});

describe('SignIn', () => {
  it('sign in success', async () => {
    const wrapper = cmp;

    wrapper.setData({
      nom: 'test',
      prenom: 'test',
      email: 'test@test.com',
      telephone: '0000000000',
      pwd: 'test',
      repeatPwd: 'test',
      isCGUAccepted: true,
      isPoliticAccepted: true
    });
    wrapper.find('.buttcreer').trigger('click');
    await wrapper.vm.$nextTick();

    var div = wrapper.find('.failed');
    expect(div.exists()).toBe(false);
  });
  it('sign in failed global', async () => {
    const wrapper = cmp;

    wrapper.setData({
      nom: '',
      prenom: '',
      email: '',
      telephone: '',
      pwd: '',
      repeatPwd: '',
      isCGUAccepted: false,
      isPoliticAccepted: false
    });
    wrapper.find('.buttcreer').trigger('click');
    await wrapper.vm.$nextTick();

    var div = wrapper.find('.failed');
    expect(div.exists()).toBe(true);
  });
  it('politics failed', async () => {
    const wrapper = cmp;

    wrapper.setData({
      nom: 'test',
      prenom: 'test',
      email: 'test@test.com',
      telephone: '0000000000',
      pwd: 'test',
      repeatPwd: 'test',
      isCGUAccepted: true,
      isPoliticAccepted: false
    });
    wrapper.find('.creer').trigger('click');
    await wrapper.vm.$nextTick();

    var pol = wrapper.find('.polFailed');
    expect(pol.exists()).toBe(true);
  });
  it('cgu failed', async () => {
    const wrapper = cmp;

    wrapper.setData({
      nom: 'test',
      prenom: 'test',
      email: 'test@test.com',
      telephone: '0000000000',
      pwd: 'test',
      repeatPwd: 'test',
      isCGUAccepted: false,
      isPoliticAccepted: true
    });
    wrapper.find('.creer').trigger('click');
    await wrapper.vm.$nextTick();

    var div = wrapper.find('.cguFailed');
    expect(div.exists()).toBe(true);
  });
  it('passwords failed', async () => {
    const wrapper = cmp;

    wrapper.setData({
      nom: 'test',
      prenom: 'test',
      email: 'test@test.com',
      telephone: '0000000000',
      pwd: 'test',
      repeatPwd: 'testes',
      isCGUAccepted: true,
      isPoliticAccepted: true
    });
    wrapper.find('.buttcreer').trigger('click');
    await wrapper.vm.$nextTick();

    var div = wrapper.find('.pwdFailed');
    expect(div.exists()).toBe(true);
  });
  it('telephone failed', async () => {
    const wrapper = cmp;

    wrapper.setData({
      nom: 'test',
      prenom: 'test',
      email: 'test@test.com',
      telephone: '00',
      pwd: 'test',
      repeatPwd: 'test',
      isCGUAccepted: true,
      isPoliticAccepted: true
    });
    wrapper.find('.buttcreer').trigger('click');
    await wrapper.vm.$nextTick();

    var div = wrapper.find('.telFailed');
    expect(div.exists()).toBe(true);
  });
  it('email failed', async () => {
    const wrapper = cmp;

    wrapper.setData({
      nom: 'test',
      prenom: 'test',
      email: 'test',
      telephone: '0000000000',
      pwd: 'test',
      repeatPwd: 'test',
      isCGUAccepted: true,
      isPoliticAccepted: true
    });
    wrapper.find('.buttcreer').trigger('click');
    await wrapper.vm.$nextTick();

    var div = wrapper.find('.emailFailed');
    expect(div.exists()).toBe(true);
  });
});
