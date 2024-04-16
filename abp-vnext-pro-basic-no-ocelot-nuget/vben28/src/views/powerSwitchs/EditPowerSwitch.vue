<template>
  <BasicModal
    :title="t('common.editText')"
    :canFullscreen="false"
    @ok="submit"
    @cancel="cancel"
    @register="registerModal"
    :minHeight="100"
  >
    <BasicForm @register="registerEditPowerSwitchForm" />
  </BasicModal>
</template>

<script lang="ts">
  import { FormSchema } from '/@/components/Table';
  import { defineComponent, reactive, watch } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { UpdatePowerSwitchFormSchema, UpdatePowerSwitch } from '/@/views/powerSwitchs/index';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { getStatusEnumValue, getIsOnlineEnumValue } from '/@/enums/powerSwitchEnum';

  export default defineComponent({
    name: 'EditPowerSwitch',
    components: {
      BasicModal,
      BasicForm,
    },
    emits: ['reload', 'register'],
    setup(_, { emit }) {
      const { t } = useI18n();
      // 使用 reactive 包裹一个对象，该对象包含你的 FormSchema 数组
      const initialFormSchema = reactive({ schemas: UpdatePowerSwitchFormSchema });
      const formModel = reactive({});

      const [
        registerEditPowerSwitchForm,
        { getFieldsValue, validate, resetFields, setFieldsValue, resetSchema },
      ] = useForm({
        labelWidth: 120,
        schemas: initialFormSchema.schemas,
        showActionButtonGroup: false,
      });
      let powerSwitchId: any;
      const [registerModal, { changeOkLoading, closeModal }] = useModalInner((data) => {
        powerSwitchId = data.record.id;
        console.log(powerSwitchId);

        const datas = data.rooms;
        console.log(datas);
        initialFormSchema.schemas[0].componentProps.options = datas.map((room) => ({
          label: room.no,
          value: room.id,
          key: room.id,
        }));

        const foundRoom = datas.find((room) => room.no === data.record.roomId);
        initialFormSchema.schemas[0].defaultValue = foundRoom.id;
        initialFormSchema.schemas[1].defaultValue = data.record.serialNumber;
        initialFormSchema.schemas[2].defaultValue = data.record.controlledMachineName;
        initialFormSchema.schemas[3].defaultValue = getIsOnlineEnumValue(data.record.isOnline);
        initialFormSchema.schemas[4].defaultValue = getStatusEnumValue(data.record.status);
        // 遍历schemas数组来初始化formModel的值
        initialFormSchema.schemas.forEach((schema) => {
          // 对于每个schema，使用其field作为键，defaultValue作为值
          formModel[schema.field] = schema.defaultValue;
        });
        console.log(formModel);
        setFieldsValue(formModel);
      });
      watch(
        () => initialFormSchema.schemas,
        async () => {
          console.log(initialFormSchema.schemas);
          console.log(UpdatePowerSwitchFormSchema);
          // 调用 useForm 返回的 resetSchema 方法更新表单项
          await resetSchema(UpdatePowerSwitchFormSchema as Partial<FormSchema>[]);
          await setFieldsValue(UpdatePowerSwitchFormSchema);
          console.log('到这里了，更新函数已完成!');
        },
        { deep: true, immediate: true },
      );
      const submit = async () => {
        try {
          const request = getFieldsValue();
          request.id = powerSwitchId;
          console.log(request);
          await UpdatePowerSwitch({ request, changeOkLoading, validate, closeModal });
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
        registerEditPowerSwitchForm,
        submit,
        cancel,
      };
    },
  });
</script>

<style lang="less" scoped></style>
