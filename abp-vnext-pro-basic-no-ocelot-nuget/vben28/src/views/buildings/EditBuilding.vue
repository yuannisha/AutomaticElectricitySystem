<template>
  <BasicModal
    :title="t('common.editText')"
    :canFullscreen="false"
    @ok="submit"
    @cancel="cancel"
    @register="registerModal"
    :minHeight="100"
  >
    <BasicForm @register="registerEditBuildingForm" />
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { UpdateBuildingFormSchema, UpdateBuilding } from '/@/views/buildings/index';
  import { useI18n } from '/@/hooks/web/useI18n';

  export default defineComponent({
    name: 'EditBuilding',
    components: {
      BasicModal,
      BasicForm,
    },
    emits: ['reload', 'register'],
    setup(_, { emit }) {
      const { t } = useI18n();
      const [registerEditBuildingForm, { getFieldsValue, validate, resetFields, setFieldsValue }] =
        useForm({
          labelWidth: 120,
          schemas: UpdateBuildingFormSchema,
          showActionButtonGroup: false,
        });
      let buildingId: any;
      const [registerModal, { changeOkLoading, closeModal }] = useModalInner((data) => {
        setFieldsValue({
          name: data.record.name,
          displayOrder: data.record.displayOrder,
        });
        buildingId = data.record.id;
      });
      console.log(buildingId);

      const submit = async () => {
        try {
          const request = getFieldsValue();
          request.id = buildingId;
          console.log(request);
          await UpdateBuilding({ request, changeOkLoading, validate, closeModal });
          await resetFields();
          emit('reload');
        } finally {
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
        registerEditBuildingForm,
        submit,
        cancel,
      };
    },
  });
</script>

<style lang="less" scoped></style>
