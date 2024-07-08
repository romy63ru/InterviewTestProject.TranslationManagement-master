/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react';
import * as React from "react";
import { TranslationJob } from "./TranslationJob";
import { Job } from "./Job";
import { accent2, gray5 } from "./Styles";

interface Props {
  data: TranslationJob[];
  renderItem?: (item: TranslationJob) => JSX.Element;
}

export const JobList = ({ data, renderItem }: Props) => (
  <div>
    <h2>JobList</h2>
    <ul  css={css`
      list-style: none;
      margin: 10px 0 0 0;
      padding: 0px 20px;
      background-color: #fff;
      border-bottom-left-radius: 4px;
      border-bottom-right-radius: 4px;
      border-top: 3px solid ${accent2};
      box-shadow: 0 3px 5px 0 rgba(0, 0, 0, 0.16);
    `}>
      {data.map((job) => (
        <li key={job.id} css={css`
          border-top: 1px solid ${gray5};
          :first-of-type {
            border-top: none;
          }
        `}>
          {renderItem ? renderItem(job) : <Job data={job} />}
        </li>
      ))}
    </ul>
  </div>
);
