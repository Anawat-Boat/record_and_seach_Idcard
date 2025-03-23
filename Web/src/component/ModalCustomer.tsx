import { Modal, Form, Input, DatePicker, Button, Spin } from "antd";
import { FormProps } from "antd/es/form";
import TextArea from "antd/es/input/TextArea";
import { useState } from "react";
import api from "../utils/api";
import dayjs, { Dayjs } from "dayjs";
type Props = {
  state: "Create" | "Update";
  id: number;
  isModalOpen: boolean;
  setIsModalOpen: React.Dispatch<React.SetStateAction<boolean>>;
};
type FieldType = {
  fullName: string;
  citizenId: string;
  birthDate: Dayjs;
  address: string;
};
const validateThaiCitizenId = (_: any, value: string): Promise<void> => {
  if (!value || value.length !== 13 || !/^\d{13}$/.test(value)) {
    return Promise.reject(new Error("Invalid Thai Citizen ID!"));
  }

  let sum: number = 0;
  for (let i = 0; i < 12; i++) {
    sum += parseInt(value[i], 10) * (13 - i); // Explicitly convert to integer
  }

  const checkDigit = (11 - (sum % 11)) % 10;
  if (checkDigit === parseInt(value[12], 10)) {
    return Promise.resolve();
  }
  return Promise.reject(new Error("Invalid Thai Citizen ID!"));
};

const ModalCustomer = ({ state, id, isModalOpen, setIsModalOpen }: Props) => {
  const [loading, setLoading] = useState<boolean>(false);
  const [form] = Form.useForm<FieldType>();

  const handleGetCustomerById = async () => {
    setLoading(true);
    api
      .get(`/api/customer/${id}`)
      .then(({ data }) => {
        onFill(data.data);
      })
      .catch((error) => console.error("Error fetching data:", error))
      .finally(() => setLoading(false));
  };

  const onFinish: FormProps<FieldType>["onFinish"] = (values) => {
    console.log("Received values from form: ", values);

    const formattedDate = values.birthDate.toISOString();
    console.log("formattedDate", formattedDate);

    setLoading(true);
    if (state === "Create") {
      api
        .post("/api/customer", { ...values, birthDate: formattedDate })
        .then(({ data }) => {
          if (data.isSuccess) {
            setIsModalOpen(false);
          }
        })
        .finally(() => setLoading(false));
    } else {
      api
        .put(`/api/customer/${id}`, values)
        .then(({ data }) => {
          if (data.isSuccess) {
            setIsModalOpen(false);
          }
        })
        .finally(() => setLoading(false));
    }
    console.log("Success:", values);
  };
  const onFinishFailed: FormProps<FieldType>["onFinishFailed"] = (
    errorInfo
  ) => {
    console.log("Failed:", errorInfo);
  };

  const onFill = (data: FieldType) => {
    form.setFieldsValue({
      fullName: data.fullName,
      citizenId: data.citizenId,
      birthDate: dayjs(data.birthDate),
      address: data.address,
    });
  };
  const onReset = () => {
    form.setFieldsValue({
      fullName: "",
      citizenId: "",
      birthDate: "",
      address: "",
    });
    form.resetFields();
    if (state === "Update") {
      handleGetCustomerById();
    }
  };
  return (
    <Modal
      open={isModalOpen}
      onCancel={() => {
        setIsModalOpen(false);
        onReset();
      }}
      onClose={() => {
        setLoading(false);
      }}
      afterOpenChange={onReset}
      title={state === "Create" ? "New Customer" : "Update Customer"}
      footer={null}
      centered
    >
      <section className="mt-5">
        <Spin spinning={loading}>
          <Form
            labelCol={{ span: 4 }}
            wrapperCol={{ span: 16 }}
            layout="horizontal"
            onFinish={onFinish}
            onFinishFailed={onFinishFailed}
            form={form}
          >
            <Form.Item<FieldType>
              label="FullName"
              name="fullName"
              rules={[{ required: true, message: "Please input your name!" }]}
            >
              <Input />
            </Form.Item>
            <Form.Item<FieldType>
              label="BirthDate"
              name="birthDate"
              rules={[
                { required: true, message: "Please input your birthday!" },
              ]}
            >
              <DatePicker />
            </Form.Item>
            <Form.Item<FieldType>
              label="CitizenId"
              name="citizenId"
              style={{ width: "100%" }}
              rules={[
                {
                  required: true,
                  message: "Please input your citizenId!",
                },
                { validator: validateThaiCitizenId },
              ]}
            >
              <Input />
            </Form.Item>
            <Form.Item<FieldType>
              label="Address"
              name="address"
              rules={[
                { required: true, message: "Please input your address!" },
              ]}
            >
              <TextArea rows={3} />
            </Form.Item>
            <Form.Item label={null}>
              <Button type="primary" htmlType="submit">
                Submit
              </Button>
            </Form.Item>
          </Form>
        </Spin>
      </section>
    </Modal>
  );
};

export default ModalCustomer;
