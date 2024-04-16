<template>
  <BasicModal
    :title="t('common.createText')"
    :canFullscreen="false"
    @ok="submit"
    @cancel="cancel"
    @register="registerModal"
  >
    <BasicForm @register="registerPowerSwitchForm" />
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent, reactive } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { CreatePowerSwitchFormSchema, CreatePowerSwitch } from './index';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { CreatePowerSwitchsInput } from '/@/services/ServiceProxies';

  export default defineComponent({
    name: 'CreatePowerSwitch',
    components: {
      BasicModal,
      BasicForm,
    },
    emits: ['reload', 'register'],
    setup(_, { emit }) {
      const { t } = useI18n();
      // 使用 reactive 包裹一个对象，该对象包含你的 FormSchema 数组
      const initialFormSchema = reactive({ schemas: CreatePowerSwitchFormSchema });

      const [registerModal, { changeOkLoading, closeModal }] = useModalInner((data)=>{
        const datas = data.rooms;
        // 更新CreatePowerSwitchFormSchema中roomId字段的options
        initialFormSchema.schemas[0].componentProps.options = datas.map((room) => ({
          label: room.no,
          value: room.id,
          key: room.id,
        }));
        console.log(initialFormSchema.schemas);
        console.log(datas[0].name);
      });

      const [registerPowerSwitchForm, { getFieldsValue, validate, resetFields }] = useForm({
        labelWidth: 120,
        schemas: initialFormSchema.schemas,
        showActionButtonGroup: false,
      });

      const submit = async () => {
        try {
          const request = getFieldsValue() as CreatePowerSwitchsInput;
          console.log(request);
          // request.name
          await CreatePowerSwitch({ request, changeOkLoading, validate, closeModal, resetFields });
          // await resetFields();
          emit('reload');
        } catch (error) {
          changeOkLoading(false);
        }
      };

      const cancel = () => {
        resetFields();
        closeModal();
      };
      return {
        t,
        registerModal,
        registerPowerSwitchForm,
        submit,
        cancel,
      };
    },
  });
</script>

<style lang="less" scoped></style>
