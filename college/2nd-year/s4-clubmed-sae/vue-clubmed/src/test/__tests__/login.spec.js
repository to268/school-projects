import { describe, it, expect } from 'vitest';

import vuex from 'vuex';
import { shallowMount } from '@vue/test-utils';
import LogIn from '@/views/LogIn.vue';
import store from '@/store/index';

const cmp = shallowMount(LogIn, {
  global: {
    plugins: [vuex],
    mocks: {
      $store: store
    }
  }
});

describe('LogIn', () => {
  it('log in success', async () => {
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
});
