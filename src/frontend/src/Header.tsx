/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react';
import React from "react";
import { Link } from "react-router-dom";
import { gray1, gray5 } from './Styles';

export const Header = () => {

return( <div>

       <div css={css`
        position: fixed;
        box-sizing: border-box;
        top: 0;
        width: 100%;
        display: flex;
        align-items: center;
        justify-content: space-between;
        padding: 10px 20px;
        background-color: #fff;
        border-bottom: 1px solid ${gray5};
        box-shadow: 0 3px 7px 0 rgba(110, 112, 114, 0.21);
      `}>
               <h1>Translation Managment</h1>
        <Link   css={css`
          font-size: 24px;
          font-weight: bold;
          color: ${gray1};
          text-decoration: none;
        `} to="/">
            Jobs
        </Link>
        <Link   css={css`
          font-size: 24px;
          font-weight: bold;
          color: ${gray1};
          text-decoration: none;
        `} to="/new">Add new job</Link>

    </div>
    </div>
)

}