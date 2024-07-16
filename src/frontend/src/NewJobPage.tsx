import React from "react";
import { useForm } from "react-hook-form";
import { Page } from "./Page";
import { createJob } from "./Data";
import { 
  FieldContainer,
  FieldInput,
  FieldLabel,
  Fieldset,
  FieldTextArea,
  FormButtonContainer,
  PrimaryButton,
  SubmissionSuccess,
} from "./Styles";
import { TranslationJob, TranslationJobImpl } from "./TranslationJob";

const NewJobPage = () => {
  const [successfullySubmitted, setSuccessfullySubmitted] = React.useState(false);

  const { handleSubmit, formState, register } = useForm<TranslationJobImpl>({
    mode: "onBlur",
  });

  const submitForm = async (data: TranslationJob) => {
    const result = await createJob(data);
    setSuccessfullySubmitted(!!result);
  };

  return (
    <Page title="Add new job">
      <form onSubmit={handleSubmit(submitForm)}>
        <Fieldset disabled={formState.isSubmitting || successfullySubmitted}>
          <FormFields register={register} />
          <FormButtonContainer>
            <PrimaryButton type="submit">Submit Your Job</PrimaryButton>
          </FormButtonContainer>
          {successfullySubmitted && (
            <SubmissionSuccess>
              Your job was successfully submitted
            </SubmissionSuccess>
          )}
        </Fieldset>
      </form>
    </Page>
  );
};

const FormFields = ({ register }) => (
  <>
    <FieldContainer>
      <FieldLabel htmlFor="customerName">Customer name</FieldLabel>
      <FieldInput id="customerName" name="customerName" type="text" {...register("customerName")} />
    </FieldContainer>
    <FieldContainer>
      <FieldLabel htmlFor="originalContent">Original Content</FieldLabel>
      <FieldTextArea id="originalContent" name="originalContent" {...register("originalContent")} />
    </FieldContainer>
    <FieldContainer>
      <FieldLabel htmlFor="translatedContent">Translated Content</FieldLabel>
      <FieldTextArea id="translatedContent" name="translatedContent" {...register("translatedContent")} />
    </FieldContainer>
    <FieldContainer>
      <FieldLabel htmlFor="price">Price</FieldLabel>
      <FieldInput id="price" name="price" type="text" {...register("price")} />
    </FieldContainer>
  </>
);

export default NewJobPage;