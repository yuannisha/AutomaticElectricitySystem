<template>
  <BasicModal
    :title="t('common.createText')"
    :canFullscreen="false"
    @ok="submit"
    @cancel="cancel"
    @register="registerModal"
  >
    <BasicForm @register="registerBuildingForm" />
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { CreateBuildingFormSchema, CreateBuilding } from './index';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { CreateBuildingsInput } from '/@/services/ServiceProxies';

  export default defineComponent({
    name: 'CreateBuilding',
    components: {
      BasicModal,
      BasicForm,
    },
    emits: ['reload', 'register'],
    setup(_, { emit }) {
      const { t } = useI18n();
      const [registerBuildingForm, { getFieldsValue, validate, resetFields }] = useForm({
        labelWidth: 120,
        schemas: CreateBuildingFormSchema,
        showActionButtonGroup: false,
      });

      const [registerModal, { changeOkLoading, closeModal }] = useModalInner();

      const submit = async () => {
        try {
          const request = getFieldsValue() as CreateBuildingsInput;
          console.log(request);
          // request.name
          await CreateBuilding({ request, changeOkLoading, validate, closeModal, resetFields });
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
        registerBuildingForm,
        submit,
        cancel,
      };
    },
  });
</script>

<style lang="less" scoped></style>
