<template>
  <BasicModal
    :title="t('common.editText')"
    :canFullscreen="false"
    @ok="submit"
    @cancel="cancel"
    @register="registerModal"
    :minHeight="100"
  >
    <BasicForm @register="registerEditRoomForm" />
  </BasicModal>
</template>

<script lang="ts">
  import { FormSchema } from '/@/components/Table';
  import { defineComponent, reactive, watch } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { UpdateRoomFormSchema, UpdateRoom } from './index';
  import { useI18n } from '/@/hooks/web/useI18n';
  import {
    getUsingOrNotEnumValue,
    getRoomTypeEnumValue,
    getControlTypeEnumValue,
  } from '/@/enums/roomEnum';

  export default defineComponent({
    name: 'EditBuilding',
    components: {
      BasicModal,
      BasicForm,
    },
    emits: ['reload', 'register'],
    setup(_, { emit }) {
      const { t } = useI18n();
      // 使用 reactive 包裹一个对象，该对象包含你的 FormSchema 数组
      const initialFormSchema = reactive({ schemas: UpdateRoomFormSchema });
      const formModel = reactive({});
      const [
        registerEditRoomForm,
        { getFieldsValue, validate, resetFields, setFieldsValue, resetSchema },
      ] = useForm({
        labelWidth: 120,
        schemas: initialFormSchema.schemas,
        showActionButtonGroup: false,
      });
      let roomId: any;

      const [registerModal, { changeOkLoading, closeModal }] = useModalInner(async (data) => {
        console.log(data.buildings);
        roomId = data.record.id;
        console.log(roomId);

        const datas = data.buildings;
        initialFormSchema.schemas[0].componentProps.options = datas.map((building) => ({
          label: building.name,
          value: building.id,
          key: building.id,
        }));
        // const options = datas.map((building) => ({
        //   label: building.name,
        //   value: building.id,
        //   key: building.id,
        // }));
        console.log(datas);
        const foundBuilding = datas.find((building) => building.name === data.record.buildingId);
        console.log(foundBuilding);
        initialFormSchema.schemas[0].defaultValue = foundBuilding.id;
        initialFormSchema.schemas[1].defaultValue = data.record.floor;
        initialFormSchema.schemas[2].defaultValue = data.record.no;
        initialFormSchema.schemas[3].defaultValue = getUsingOrNotEnumValue(
          data.record.isUsingOrNot,
        );
        initialFormSchema.schemas[4].defaultValue = getRoomTypeEnumValue(data.record.roomType);
        initialFormSchema.schemas[5].defaultValue = getControlTypeEnumValue(
          data.record.controlType,
        );
        // console.log(initialFormSchema.schemas);
        // console.log(UpdateRoomFormSchema);
        // await updateSchema(UpdateRoomFormSchema);
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
          console.log(UpdateRoomFormSchema);
          // 调用 useForm 返回的 resetSchema 方法更新表单项
          await resetSchema(UpdateRoomFormSchema as Partial<FormSchema>[]);
          await setFieldsValue(UpdateRoomFormSchema);
          console.log('到这里了，更新函数已完成!');
        },
        { deep: true, immediate: true },
      );
      const submit = async () => {
        try {
          const request = getFieldsValue();
          request.id = roomId;
          console.log(request);
          await UpdateRoom({ request, changeOkLoading, validate, closeModal });
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
        registerEditRoomForm,
        submit,
        cancel,
      };
    },
  });
</script>

<style lang="less" scoped></style>
